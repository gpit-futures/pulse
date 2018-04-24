import React, { PureComponent } from 'react';
import { connect } from "react-redux";
import { Field, reduxForm } from 'redux-form'
import moment from "moment";

import ValidatedTextareaFormGroup from '../../../form-fields/ValidatedTextareaFormGroup';
import SelectFormGroup from '../../../form-fields/SelectFormGroup';
import DateInput from '../../../form-fields/DateInput';
import RecordsOfTable from '../../../form-fields/RecordsOfTable/RecordsOfTable';
import { validateForm } from '../forms.validation';
import { valuesNames, valuesLabels, citiesOptions, typesOfRecordsOptions } from '../forms.config';
import { transfersOfCareDetailFormStateSelector} from "../selectors";

@reduxForm({
  form: 'transfersOfCareDetailFormSelector',
  validate: validateForm,
})
@connect(transfersOfCareDetailFormStateSelector)
export default class TransfersOfCareDetailForm extends PureComponent {
  componentDidMount() {
    const { detail, initialize } = this.props;
    initialize(this.defaultValuesForm(detail));
  }

  defaultValuesForm = (value) => {
    const defaultFormValues = {
      [valuesNames.FROM]: value[valuesNames.FROM],
      [valuesNames.TO]: value[valuesNames.TO],
      [valuesNames.RECORDS]: value[valuesNames.RECORDS],
      [valuesNames.REASON]: value[valuesNames.REASON],
      [valuesNames.CLINICAL]: value[valuesNames.CLINICAL],
      [valuesNames.DATE_TIME]: value[valuesNames.DATE_TIME],
      [valuesNames.DATE_CREATED]: value[valuesNames.DATE_CREATED],
    };

    return defaultFormValues;
  };

  generateCitiesOptions = (selected) => {
    return citiesOptions.slice().map(item => ({
      ...item,
      disabled: (item.value === selected)
    }));
  };

  render() {
    const { detail, isSubmit, transfersOfCareDetailFormState, match } = this.props;

    const formState = transfersOfCareDetailFormState.values || {};
    const citiesFromOptions = this.generateCitiesOptions(formState[valuesNames.TO]);
    const citiesToOptions = this.generateCitiesOptions(formState[valuesNames.FROM]);

    return (
      <div className="panel-body-inner">
        <form name="transfersOfCareDetailForm" className="form">
          <div className="form-group-wrapper">

            <div className="row-expand">
              <div className="col-expand-left">
                <Field
                  label={valuesLabels.FROM}
                  name={valuesNames.FROM}
                  id={valuesNames.FROM}
                  options={citiesFromOptions}
                  component={SelectFormGroup}
                  placeholder="-- Select from --"
                  props={{ isSubmit }}
                />
              </div>
              <div className="col-expand-right">
                <Field
                  label={valuesLabels.TO}
                  name={valuesNames.TO}
                  id={valuesNames.TO}
                  options={citiesToOptions}
                  component={SelectFormGroup}
                  placeholder="-- Select to --"
                  props={{ isSubmit }}
                />
              </div>
            </div>

            <Field
              label={valuesLabels.DATE_TIME}
              name={valuesNames.DATE_TIME}
              id={valuesNames.DATE_TIME}
              component={DateInput}
              showTimeSelect
              props={{
                format: 'DD-MMM-YYYY HH:mm', isSubmit, showTimeSelect: true,
                timeFormat: 'HH:mm', timeIntervals: 5, minDate: moment() }}
            />

            <Field
              name={valuesNames.RECORDS}
              id={valuesNames.RECORDS}
              component={RecordsOfTable}
              props={{ match, isSubmit, typesOptions: typesOfRecordsOptions }}
            />

            <div className="row-expand">
              <div className="col-expand-left">
                <Field
                  label={valuesLabels.REASON}
                  name={valuesNames.REASON}
                  id={valuesNames.REASON}
                  component={ValidatedTextareaFormGroup}
                  props={{ isSubmit }}
                />
              </div>
              <div className="col-expand-right">
                <Field
                  label={valuesLabels.CLINICAL}
                  name={valuesNames.CLINICAL}
                  id={valuesNames.CLINICAL}
                  component={ValidatedTextareaFormGroup}
                  props={{ isSubmit }}
                />
              </div>
            </div>

            <Field
              label={valuesLabels.DATE_CREATED}
              name={valuesNames.DATE_CREATED}
              id={valuesNames.DATE_CREATED}
              component={DateInput}
              props={{ disabled: true, value: detail[valuesNames.DATE_CREATED], format: 'DD-MMM-YYYY', isSubmit }}
            />
          </div>
        </form>
      </div>)
  }
}
