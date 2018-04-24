import React from 'react';
import PropTypes from 'prop-types';
import _ from 'lodash/fp';

const SimpleDashboardPanel = ({ title, items, goToState, state, isHasPreview, isHasList, srcPrevirew }) => {
  return (<div className="dashboard-item">
    <div className="board">
      <div className="board-header">
        <div className="control-group right">
          <button className="btn btn-success btn-inverse btn-board-more" onClick={() => goToState(state)}><i className="btn-icon fa fa-caret-right" /></button>
        </div>
        <h3 className="board-title">{ title }</h3>
      </div>
      <div className="board-body">
        {isHasPreview
          ? <div
            className="board-preview"
            style={{ backgroundImage: `url(${srcPrevirew})` }}
            onClick={() => goToState(state)}
          />
          : null
        }
        {isHasList
          ? <ul className="board-list">
            {items.map(item =>
              <li className="board-list-item" key={_.uniqueId('__SimpleDashboardPanel__item__')}>
                {item.text ? <span className="board-list-link" onClick={() => goToState(`${state}/${item.sourceId}`, item.link)} title={item.text}>{item.text}</span> : null}
              </li>)}
          </ul>
          : null}
      </div>
    </div>
  </div>)
};

SimpleDashboardPanel.propTypes = {
  title: PropTypes.string.isRequired,
  items: PropTypes.array.isRequired,
};

export default SimpleDashboardPanel
