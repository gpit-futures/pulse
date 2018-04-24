import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientPersonalNotesRequest } from './fetch-patient-personal-notes.duck'

export const FETCH_PATIENT_PERSONAL_NOTES_CREATE_REQUEST = 'FETCH_PATIENT_PERSONAL_NOTES_CREATE_REQUEST';
export const FETCH_PATIENT_PERSONAL_NOTES_CREATE_SUCCESS = 'FETCH_PATIENT_PERSONAL_NOTES_CREATE_SUCCESS';
export const FETCH_PATIENT_PERSONAL_NOTES_CREATE_FAILURE = 'FETCH_PATIENT_PERSONAL_NOTES_CREATE_FAILURE';

export const fetchPatientPersonalNotesCreateRequest = createAction(FETCH_PATIENT_PERSONAL_NOTES_CREATE_REQUEST);
export const fetchPatientPersonalNotesCreateSuccess = createAction(FETCH_PATIENT_PERSONAL_NOTES_CREATE_SUCCESS);
export const fetchPatientPersonalNotesCreateFailure = createAction(FETCH_PATIENT_PERSONAL_NOTES_CREATE_FAILURE);

export const fetchPatientPersonalNotesCreateEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_PERSONAL_NOTES_CREATE_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.post(`${usersUrls.PATIENTS_URL}/${payload.userId}/personalnotes`, payload, {
        Cookie: store.getState().credentials.cookie,
        'Content-Type': 'application/json',
      })
        .flatMap(({ response }) => {
          const userId = payload.userId;

          return [
            fetchPatientPersonalNotesCreateSuccess(response),
            fetchPatientPersonalNotesRequest({ userId }),
          ];
        })
    );

export default function reducer(patientPersonalNotesCreate = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_PERSONAL_NOTES_CREATE_SUCCESS:
      return action.payload;
    default:
      return patientPersonalNotesCreate
  }
}
