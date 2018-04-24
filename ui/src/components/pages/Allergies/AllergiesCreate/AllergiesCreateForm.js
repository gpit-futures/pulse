import React, { PureComponent } from 'react';
import { Field, reduxForm } from 'redux-form'
import { Row, Col } from 'react-bootstrap';

import ValidatedInput from '../../../form-fields/ValidatedInputFormGroup';
import ValidatedTextareaFormGroup from '../../../form-fields/ValidatedTextareaFormGroup';
import DateInput from '../../../form-fields/DateInput';
import StaticFormField from '../../../form-fields/StaticFormField';
import { validateAllergiesCreateForm } from '../forms.validation';
import { valuesNames, valuesLabels } from '../forms.config';
import { defaultFormValues } from './default-values.config';
import PropTypes from "prop-types";

@reduxForm({
  form: 'allergiesCreateFormSelector',
  validate: validateAllergiesCreateForm,
})
export default class AllergiesCreateForm extends PureComponent {
  static contextTypes = {
    router: PropTypes.shape({
      history: PropTypes.object,
    }),
    history: PropTypes.object,
  };

  state = {
    isImport: false
  };

  componentDidMount() {
    const historyState = this.context.router.history.location.state;

    if (historyState && historyState.importData) {
      const dataToInitialize = {
        ...defaultFormValues,
        ...historyState.importData
      };
      if (!historyState.importData[valuesNames.TERMINOLOGY]) {
        dataToInitialize[valuesNames.TERMINOLOGY] = defaultFormValues[valuesNames.TERMINOLOGY];
      }
      if (!historyState.importData[valuesNames.TERMINOLOGYCODE]) {
        dataToInitialize[valuesNames.TERMINOLOGYCODE] = defaultFormValues[valuesNames.TERMINOLOGYCODE];
      }
      this.props.initialize(dataToInitialize);
      this.setState({isImport: true})

    } else {
      this.props.initialize(defaultFormValues);
    }
  }

  render() {
    const {isSubmit} = this.props;
    const {isImport} = this.state;

    const date = new Date();
    const dateCreated = date.getTime();

    return (
      <div className="panel-body-inner">
        <form name="allergiesCreateForm" className="form">
          <div className="form-group-wrapper">
            <Row>
              <Col md={6}>
                <Field
                  label={valuesLabels.CAUSE}
                  name={valuesNames.CAUSE}
                  id={valuesNames.CAUSE}
                  component={ValidatedInput}
                  props={{ isSubmit }}
                />
              </Col>
              <Col md={6}>
                <Field
                  name={valuesNames.CAUSECODE}
                  label={valuesLabels.CAUSECODE}
                  component={StaticFormField}
                  props={{ className: 'form-control-static', isSubmit }}
                />
              </Col>
            </Row>
            <Field
              label={valuesLabels.REACTION}
              name={valuesNames.REACTION}
              id={valuesNames.REACTION}
              component={ValidatedTextareaFormGroup}
              props={{ isSubmit }}
            />
            <Row>
              <Col md={6}>
                <Field
                  name={valuesNames.TERMINOLOGY}
                  label={valuesLabels.TERMINOLOGY}
                  component={StaticFormField}
                  props={{ className: 'form-control-static' }}
                />
              </Col>
              <Col md={6}>
                <Field
                  name={valuesNames.TERMINOLOGYCODE}
                  label={valuesLabels.TERMINOLOGYCODE}
                  component={StaticFormField}
                  props={{ className: 'form-control-static' }}
                />
              </Col>
            </Row>
            {isImport ?
              <Field
                label={valuesLabels.IMPORT}
                name={valuesNames.IMPORT}
                id={valuesNames.IMPORT}
                component={ValidatedInput}
                props={{ disabled: true, isSubmit }}
              />
              : null
            }
            <Field
              label={valuesLabels.AUTHOR}
              name={valuesNames.AUTHOR}
              id={valuesNames.AUTHOR}
              component={ValidatedInput}
              props={{ disabled: true }}
            />
            <Field
              label={valuesLabels.DATE}
              name={valuesNames.DATE}
              id={valuesNames.DATE}
              component={DateInput}
              props={{ disabled: true, value: dateCreated, format: 'DD-MMM-YYYY' }}
            />
          </div>
        </form>
      </div>)
  }
}
