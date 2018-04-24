import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../config/server-urls.constants'

export const FETCH_PATIENTS_REQUEST = 'FETCH_PATIENTS_REQUEST';
export const FETCH_PATIENTS_SUCCESS = 'FETCH_PATIENTS_SUCCESS';
export const FETCH_PATIENTS_FAILURE = 'FETCH_PATIENTS_FAILURE';

export const fetchPatientsRequest = createAction(FETCH_PATIENTS_REQUEST);
export const fetchPatientsSuccess = createAction(FETCH_PATIENTS_SUCCESS);
export const fetchPatientsFailure = createAction(FETCH_PATIENTS_FAILURE);

export const fetchPatientsEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENTS_REQUEST)
    .mergeMap(() =>
      ajax.getJSON(usersUrls.PATIENTS_URL, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map(fetchPatientsSuccess)
    );

export default function reducer(patients = {}, action) {
  switch (action.type) {
    case FETCH_PATIENTS_SUCCESS:
      return action.payload;
    default:
      return patients;
  }
}
