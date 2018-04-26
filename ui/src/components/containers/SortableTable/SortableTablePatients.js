import React, { PureComponent } from 'react';
import PropTypes from 'prop-types';
import _ from 'lodash/fp';

import SortableTableHeaderRow from './sortable-table-header-components/SortableTableHeaderRow';
import SortableTableHoveredRow from './SortableTableHoveredRow';
import SortableTableEmptyDataRow from './SortableTableEmptyDataRow';
import { getArrByTemplate } from '../../../utils/table-helpers/table.utils';

export default class SortableTablePatients extends PureComponent {
  static propTypes = {
    headers: PropTypes.arrayOf(PropTypes.object).isRequired,
    data: PropTypes.arrayOf(PropTypes.object).isRequired,
    onHeaderCellClick: PropTypes.func.isRequired,
    onCellClick: PropTypes.func.isRequired,
    sortingOrder: PropTypes.oneOf(['asc', 'desc']).isRequired,
    columnNameSortBy: PropTypes.string.isRequired,
    table: PropTypes.string.isRequired,
  };

  static defaultProps = {
    table: '',
  };

  state = {
    hoveredRowIndex: '',
  };

  getSortableTableRows = (rowsData, resourceData, emptyDataMessage) => {
    const { onCellClick, columnNameSortBy, headers, table } = this.props;
    const { hoveredRowIndex } = this.state;
    const amountCollumns = headers.length;

    return (
      _.isUndefined(resourceData)
        ? <SortableTableEmptyDataRow isLoading emptyDataMessage={emptyDataMessage} amountCollumns={amountCollumns} />
        : !resourceData.length
          ? <SortableTableEmptyDataRow isLoading={false} emptyDataMessage={emptyDataMessage} amountCollumns={amountCollumns} />
          : rowsData.map((rowData, index) =>
            <SortableTableHoveredRow
              key={_.uniqueId('__SortableTableRow__')}
              rowData={rowData}
              onCellClick={onCellClick}
              columnNameSortBy={columnNameSortBy}
              headers={headers}
              onMouseEnter={this.hoverTableRow}
              onMouseLeave={this.unHoverTableRow}
              hoveredRowIndex={hoveredRowIndex}
              index={index}
              table={table}
              resourceData={resourceData}
            />)
    )
  };

  hoverTableRow = (index) => {
    this.setState({ hoveredRowIndex: index });
  };

  unHoverTableRow = () => {
    this.setState({ hoveredRowIndex: '' });
  };

  resizeFixedTables = () => {
    const tableNames = this.tableNames;
    const tableControls = this.tableControls;
    const tableFull = this.tableFull;

    if (tableNames && tableControls && tableFull) {
      const tableFullRows = _.last(tableFull.children).children;
      const tds = _.head(tableFullRows).children;

      tableNames.style.width = `${_.head(tds).offsetWidth + 2}px`;
      tableControls.style.width = `${tds[tds.length - 1].offsetWidth + 1}px`;
    }
  };

  render() {
    const { headers, data, onHeaderCellClick, sortingOrder, columnNameSortBy, table, resourceData, emptyDataMessage } = this.props;
    const rowsData = getArrByTemplate(headers, data, '-');
    const headersName = [_.head(headers)];
    const headersView = [];
    const rowsDataName = rowsData.map(el => el.filter(el => el.name === 'name'));
    const rowsDataView = rowsData.map(el => el.filter(el => el.name === 'viewPatientNavigation'));

    setTimeout(() => this.resizeFixedTables());
    window.addEventListener('resize', () => {
      this.resizeFixedTables()
    });

    return (
      <div>
        
        <table
          className={`table table-striped table-bordered table-sorted table-hover table-fixedcol table-patients-full rwd-table ${table}`}
          ref={(el) => { this.tableFull = el; }}
        >
          <colgroup>
            {/*//TODO inject theme here*/}
            {headers.map((item) => {
              return (<col style={{ width: item.width, display: item.display, minWidth: item.minWidth }} key={_.uniqueId('__colHeaders__')} />)
            })}
          </colgroup>
          <thead>
            <SortableTableHeaderRow
              headers={headers}
              onHeaderCellClick={onHeaderCellClick}
              sortingOrder={sortingOrder}
              columnNameSortBy={columnNameSortBy}
            />
          </thead>
          <tbody>
            {this.getSortableTableRows(rowsData, resourceData, emptyDataMessage)}
          </tbody>
        </table>
        
      </div>)
  }
}

