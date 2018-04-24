import React from 'react';
import Enzyme, { shallow } from 'enzyme';
import Adapter from 'enzyme-adapter-react-15';

import SortableTableHeaderRow from '../sortable-table-header-components/SortableTableHeaderRow';

Enzyme.configure({ adapter: new Adapter() });

const headers = [
  {
    key: 'cause',
    title: 'Cause',
    width: '25%',
  },
  {
    key: 'reaction',
    title: 'Reaction',
    width: '65%',
  },
  {
    display: 'none',
    key: 'sourceId',
    title: 'Source ID',
    width: '0',
  },
];
const onHeaderCellClick = () => console.log('test');

describe('Component <SortableTableHeaderRow />', () => {
  it('should renders shallow correctly', () => {
    const sortableTableHeaderRow = shallow(
      <SortableTableHeaderRow
        columnNameSortBy="cause"
        headers={headers}
        sortingOrder="asc"
        onHeaderCellClick={onHeaderCellClick}
      />);
    sortableTableHeaderRow.find('[name="cause"]').simulate('click');
    expect(sortableTableHeaderRow).toMatchSnapshot();
  });
});

