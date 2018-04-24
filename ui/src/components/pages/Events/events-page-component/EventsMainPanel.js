import React, { PureComponent } from 'react';
import _ from 'lodash/fp';
import classNames from 'classnames';
import { Range } from 'rc-slider';
import moment from 'moment';

import PTButton from '../../../ui-elements/PTButton/PTButton';
import SortableTable from '../../../containers/SortableTable/SortableTable';
import PaginationBlock from '../../../presentational/PaginationBlock/PaginationBlock';
import Spinner from '../../../ui-elements/Spinner/Spinner';
import Timelines from './EventsTimelines';
import { getMarksArray } from '../events-helpers.utils'

const CREATE_CONTENT = 'createContent';

export default class EventsMainPanel extends PureComponent {
  static defaultProps = {
    listPerPageAmount: 10,
    emptyDataMessage: 'No list',
  };

  getEventsOnFirstPage = (list) => {
    const { listPerPageAmount, offset } = this.props;

    return (_.size(list) > listPerPageAmount
      ? _.slice(offset, offset + listPerPageAmount)(list)
      : list)
  };

  state = {
    openedPanel: '',
    activeCreate: '',
    rangeForm: null,
    rangeTo: null,
  };

  componentWillMount() {
    document.addEventListener('click', this.handleClick, false);
  }

  componentWillReceiveProps(nextProps) {
    const { eventsType } = this.props;
    if (eventsType !== nextProps.eventsType) {
      this.setState({ openedPanel: '' });
    }
  }

  componentWillUnmount() {
    document.removeEventListener('click', this.handleClick, false);
  }

  handleClick = /* istanbul ignore next */ (e) => {
    if (!this.node.contains(e.target)) {
      this.setState({ openedPanel: '' });
    }
  };

  handleMouseDown = (name) => {
    this.setState((prevState) => {
      if (prevState.openedPanel !== name) {
        return ({ openedPanel: name })
      }
      return ({ openedPanel: '' })
    })
  };

  changeRange = (value) => {
    const { onRangeChange } = this.props;

    this.setState({ rangeForm: value[0], rangeTo: value[1] });
    onRangeChange(value);
  };

  shouldHavePagination = list => _.size(list) > this.props.listPerPageAmount;

  render() {
    const { openedPanel } = this.state;
    const { headers, resourceData, emptyDataMessage, onHeaderCellClick, onCellClick, columnNameSortBy, sortingOrder, filteredData, totalEntriesAmount, offset, setOffset, onCreate, listPerPageAmount, isLoading, id, eventsTimeline, activeView, eventsType, isTimelinesOpen, minValueRange, maxValueRange } = this.props;
    const listOnFirstPage = _.flow(this.getEventsOnFirstPage)(filteredData);

    const min = (!_.isEmpty(resourceData)) ? new Date(Math.min(...resourceData.map(item => item.dateTime))).getTime() : 0;
    const max = (!_.isEmpty(resourceData)) ? new Date(Math.max(...resourceData.map(item => item.dateTime))).getTime() : 0;

    const marks = getMarksArray(min, max);

    return (
      <div className="panel-body">
        {isTimelinesOpen ? <div className="wrap-rzslider">
          {(minValueRange !== 0 && maxValueRange !== 0)
            ? <div>
              <div className="rzslider-tooltip rzslider-tooltip--left">
                <div className="rzslider-tooltip-content">
                  <div className="rzslider-tooltip-inner">From: {moment(this.state.rangeForm || minValueRange).format('DD MMM YYYY')}</div>
                </div>
              </div>
              <div className="rzslider-tooltip rzslider-tooltip--right">
                <div className="rzslider-tooltip-content">
                  <div className="rzslider-tooltip-inner">To: {moment(this.state.rangeTo || maxValueRange).format('DD MMM YYYY')}</div>
                </div>
              </div>
              <div className="wrap-rzslider-events"><Range
                min={min}
                max={max}
                defaultValue={[min, max]}
                marks={marks}
                tipFormatter={value => `${moment(value).format('DD MMMM YYYY')}`}
                tipProps={{ visible: true, defaultVisible: true }}
                onChange={this.changeRange}
              /></div>
            </div> : null }
        </div> : null}
        {activeView === 'table' ? <SortableTable
          headers={headers}
          data={listOnFirstPage}
          resourceData={resourceData}
          emptyDataMessage={emptyDataMessage}
          onHeaderCellClick={onHeaderCellClick}
          onCellClick={onCellClick}
          columnNameSortBy={columnNameSortBy}
          sortingOrder={sortingOrder}
          id={id}
        /> : null }
        {isLoading ? <Spinner /> : null }
        {activeView === 'timeline' ? <Timelines
          eventsTimeline={eventsTimeline}
          onCellClick={onCellClick}
          id={id}
        /> : null }
        <div className="panel-control">
          <div className="wrap-control-group">
            {(this.shouldHavePagination(filteredData) && activeView === 'table') ? <div className="control-group with-indent left">
              <PaginationBlock
                entriesPerPage={listPerPageAmount}
                totalEntriesAmount={totalEntriesAmount}
                offset={offset}
                setOffset={setOffset}
              />
            </div> : null }
            <div className="control-group with-indent right" ref={node => this.node = node}>
              <div className={classNames('dropdown', { 'open': openedPanel === CREATE_CONTENT })}>
                <PTButton className="btn btn-success btn-dropdown-toggle btn-table" onClick={() => this.handleMouseDown(CREATE_CONTENT)}>
                  <span className="btn-text">Create</span>
                </PTButton>
                <div className="dropdown-menu dropdown-menu-top-right dropdown-menu-small-size">
                  <div className="dropdown-menu-wrap-list">
                    <div className="dropdown-menu-list">
                      <div className={classNames('dropdown-menu-item', { 'active': eventsType === 'Appointment' })} onClick={() => onCreate('Appointment')}><span className="dropdown-menu-item-text">Appointment</span></div>
                      <div className={classNames('dropdown-menu-item', { 'active': eventsType === 'Admission' })} onClick={() => onCreate('Admission')}><span className="dropdown-menu-item-text">Admission</span></div>
                      <div className={classNames('dropdown-menu-item', { 'active': eventsType === 'Transfer' })} onClick={() => onCreate('Transfer')}><span className="dropdown-menu-item-text">Transfer</span></div>
                      <div className={classNames('dropdown-menu-item', { 'active': eventsType === 'Discharge' })} onClick={() => onCreate('Discharge')}><span className="dropdown-menu-item-text">Discharge</span></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    )
  }
}
