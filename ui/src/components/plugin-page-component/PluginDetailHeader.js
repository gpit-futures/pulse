import React, { PureComponent } from 'react';

import PTButton from '../ui-elements/PTButton/PTButton';

export default class PluginDetailHeader extends PureComponent {
  render() {
    const { onExpand, name, title, onShow, currentPanel, isBtnShowPanel } = this.props;

    return (
      <div className="panel-heading">
        <div className="control-group right">
          <PTButton className="btn btn-success btn-inverse btn-square hidden-xs hidden-sm btn-expand-panel" onClick={() => onExpand(name, currentPanel)}>
            <i className="btn-icon fa fa-expand" />
            <i className="btn-icon fa fa-compress" />
          </PTButton>
          {isBtnShowPanel ? <PTButton className="btn btn-success btn-inverse btn-square btn-toggle-rotate" onClick={() => onShow(name)}>
            <i className="btn-icon fa fa-chevron-up" />
          </PTButton> : null }
        </div>
        <h3 className="panel-title">{title}</h3>
      </div>
    )
  }
}
