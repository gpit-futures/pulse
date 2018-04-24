import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { hasTokenInResponse } from '../../../../utils/plugin-helpers.utils';

export const FETCH_PATIENT_TEST_RESULTS_REQUEST = 'FETCH_PATIENT_TEST_RESULTS_REQUEST';
export const FETCH_PATIENT_TEST_RESULTS_SUCCESS = 'FETCH_PATIENT_TEST_RESULTS_SUCCESS';
export const FETCH_PATIENT_TEST_RESULTS_FAILURE = 'FETCH_PATIENT_TEST_RESULTS_FAILURE';

export const fetchPatientTestResultsRequest = createAction(FETCH_PATIENT_TEST_RESULTS_REQUEST);
export const fetchPatientTestResultsSuccess = createAction(FETCH_PATIENT_TEST_RESULTS_SUCCESS);
export const fetchPatientTestResultsFailure = createAction(FETCH_PATIENT_TEST_RESULTS_FAILURE);

export const fetchPatientTestResultsEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_TEST_RESULTS_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/labresults`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map((response) => {
          const token = hasTokenInResponse(response);
          return fetchPatientTestResultsSuccess({
            userId: payload.userId,
            testResults: response,
            token,
          })
        })
    );

export default function reducer(patientsTestResults = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_TEST_RESULTS_SUCCESS:
      return _.set(action.payload.userId, action.payload.testResults, patientsTestResults);
    default:
      return patientsTestResults;
  }
}
