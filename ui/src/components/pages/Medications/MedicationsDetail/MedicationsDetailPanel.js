import React, { PureComponent } from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import _ from 'lodash/fp';

import PTButton from '../../../ui-elements/PTButton/PTButton';
import PluginDetailHeader from '../../../plugin-page-component/PluginDetailHeader';

export default class MedicationsDetailPanel extends PureComponent {
  static propTypes = {
    name: PropTypes.string.isRequired,
    title: PropTypes.string.isRequired,
    isOpen: PropTypes.bool.isRequired,
    children: PropTypes.element.isRequired,
    onShow: PropTypes.func.isRequired,
    onExpand: PropTypes.func.isRequired,
    onEdit: PropTypes.func.isRequired,
    onCancel: PropTypes.func.isRequired,
    onSaveSettings: PropTypes.func.isRequired,
    editedPanel: PropTypes.object,
    isShowControlPanel: PropTypes.bool,
  };

  static defaultProps = {
    isShowControlPanel: true,
  };

  render() {
    const { name, title, children, isOpen, onShow, onExpand, onEdit, editedPanel, onCancel, onSaveSettings, formValues, currentPanel, isCreatePanelVisible, isBtnShowPanel, isShowControlPanel } = this.props;

    return (
      <div className={classNames('panel panel-secondary', { open: isOpen })}>
        <PluginDetailHeader onExpand={onExpand} name={name} title={title} onShow={onShow} currentPanel={currentPanel} isBtnShowPanel={isBtnShowPanel} />
        <div className="panel-body">
          {children}
          {(isShowControlPanel && !isCreatePanelVisible && (_.isUndefined(editedPanel[name]) || !editedPanel[name])) ? <div className="panel-control">
            <div className="wrap-control-group hide-indent-bottom">
              <div className="control-group with-indent left hidden-xs">
                <PTButton className="btn btn-danger">
                  <i className="btn-icon fa fa-close" />
                  <span className="btn-text"> Cancel</span>
                </PTButton>
                <PTButton className="btn btn-success btn-pause">
                  <i className="btn-icon fa fa-pause" />
                  <span className="btn-text"> Suspend</span>
                </PTButton>
                <PTButton className="btn btn-success">
                  <i className="btn-icon fa fa-share" />
                  <span className="btn-text"> Order</span>
                </PTButton>
              </div>
              <div className="control-group with-indent right">
                <PTButton className="btn btn-success btn-inverse btn-edit" onClick={() => onEdit(name)}>
                  <i className="btn-icon fa fa-edit" />
                  <span className="btn-text"> Edit</span>
                </PTButton>
              </div>
            </div>
          </div> : null }
          {(isShowControlPanel && !isCreatePanelVisible && editedPanel[name]) ? <div className="panel-control ng-scope">
            <div className="wrap-control-group">
              <div className="control-group right">
                <PTButton className="btn btn-danger" onClick={() => onCancel(name)}>
                  <i className="btn-icon fa fa-ban" />
                  <span className="btn-text"> Cancel</span>
                </PTButton>
                <PTButton className="btn btn-success" onClick={() => onSaveSettings(formValues, name)}>
                  <i className="btn-icon fa fa-check" />
                  <span className="btn-text"> Complete</span>
                </PTButton>
              </div>
            </div>
          </div> : null }
        </div>
      </div>
    )
  }
}
