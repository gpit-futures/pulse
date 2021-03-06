import React, { PureComponent } from 'react';
import classNames from 'classnames';

import SearchOptions from './SearchOptions'
import PTButton from '../../ui-elements/PTButton/PTButton';
import BasicPatientSearch from '../BasicPatientSearch/BasicPatientSearch';
import AdvancedPatientSearch from '../AdvancedPatientSearch/AdvancedPatientSearch';
import ClinicalQuerySearch from '../ClinicalQuerySearch/ClinicalQuerySearch';

const BASIC_SEARCH = 'basicSearch';
const ADVANCED_SEARCH = 'advancedSearch';
const SEARCH_CONTENT = 'searchContent';

export default class NavSearch extends PureComponent {
  state = {
    selected: BASIC_SEARCH,
    openedPanel: '',
  };

  handleSelect = (selected) => {
    this.setState({ selected });
    //TODO remove this spike to close dropdown
    document.body.click();
  };

  componentWillMount() {
    document.addEventListener('click', this.handleClick, false);
  }

  componentWillUnmount() {
    document.removeEventListener('click', this.handleClick, false);
  }

  handleClick = (e) => {
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


  render() {
    const { selected, openedPanel } = this.state;

    return <div className="wrap-search wrap-header-search" ref={node => this.node = node}>
      <div className="header-search">
        <BasicPatientSearch />
      </div>
    </div>
  }
}
