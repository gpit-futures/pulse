import React, { PureComponent } from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';

export default class ValidatedTextareaFormGroup extends PureComponent {
  static propTypes = {
    label: PropTypes.string.isRequired,
    input: PropTypes.object.isRequired,
    meta: PropTypes.shape({
      active: PropTypes.bool,
      error: PropTypes.any,
    }).isRequired,
  };

  state = {
    isChanged: false,
  };

  componentWillReceiveProps(nextProps) {
    if (nextProps.meta.dirty) {
      this.setState({ isChanged: true });
    }
  }

  render() {
    const { label, input, meta: { error, touched }, id, isSubmit, isAdvancedSearch } = this.props;
    const { isChanged } = this.state;
    const showError = ((touched || isChanged || isSubmit) && error);

    return (
      <div className={classNames('form-group', { 'has-error': showError }, { 'has-success': isChanged && !error })}>
        <label htmlFor={id} className="control-label">{label}</label>
        <div className="input-holder">
          <textarea
            className="form-control textarea-big input-sm"
            id={id}
            {...input}
          />
        </div>
        {(showError && isAdvancedSearch) ? <span className="required-label">{error}</span> : null}
        {(showError && !isAdvancedSearch) ? <span className="help-block animate-fade">{error}</span> : null}
      </div>
    )
  }
}
