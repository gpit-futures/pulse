import React, { PureComponent } from 'react';
import PropTypes from 'prop-types';

export default class StaticFormField extends PureComponent {
  static propTypes = {
    label: PropTypes.string.isRequired,
    input: PropTypes.object.isRequired,
  };

  render() {
    const { label, input, className} = this.props;
    return (
      <div className="form-group">
        <label className="control-label">{label}</label>
        <div className={className} {...input}>{input.value}</div>
      </div>
    )
  }
}
