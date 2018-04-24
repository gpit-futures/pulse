import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientPersonalNotesDetailRequest } from './fetch-patient-personal-notes-detail.duck';
import { hasTokenInResponse } from '../../../../utils/plugin-helpers.utils';

export const FETCH_PATIENT_PERSONAL_NOTES_REQUEST = 'FETCH_PATIENT_PERSONAL_NOTES_REQUEST';
export const FETCH_PATIENT_PERSONAL_NOTES_SUCCESS = 'FETCH_PATIENT_PERSONAL_NOTES_SUCCESS';
export const FETCH_PATIENT_PERSONAL_NOTES_FAILURE = 'FETCH_PATIENT_PERSONAL_NOTES_FAILURE';
export const FETCH_PATIENT_PERSONAL_NOTES_UPDATE_REQUEST = 'FETCH_PATIENT_PERSONAL_NOTES_UPDATE_REQUEST';

export const fetchPatientPersonalNotesRequest = createAction(FETCH_PATIENT_PERSONAL_NOTES_REQUEST);
export const fetchPatientPersonalNotesSuccess = createAction(FETCH_PATIENT_PERSONAL_NOTES_SUCCESS);
export const fetchPatientPersonalNotesFailure = createAction(FETCH_PATIENT_PERSONAL_NOTES_FAILURE);
export const fetchPatientPersonalNotesUpdateRequest = createAction(FETCH_PATIENT_PERSONAL_NOTES_UPDATE_REQUEST);

export const fetchPatientPersonalNotesEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_PERSONAL_NOTES_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/personalnotes`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map((response) => {
          const token = hasTokenInResponse(response);
          return fetchPatientPersonalNotesSuccess({
            userId: payload.userId,
            personalNotes: response,
            token,
          })
        })
    );

export const fetchPatientPersonalNotesUpdateEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_PERSONAL_NOTES_UPDATE_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/personalnotes`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .flatMap((response) => {
          const userId = payload.userId;
          const sourceId = payload.sourceId;

          return [
            fetchPatientPersonalNotesSuccess({ userId, personalNotes: response }),
            fetchPatientPersonalNotesDetailRequest({ userId, sourceId }),
          ]
        })

    );

export default function reducer(patientsPersonalNotes = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_PERSONAL_NOTES_SUCCESS:
      return _.set(action.payload.userId, action.payload.personalNotes, patientsPersonalNotes);
    default:
      return patientsPersonalNotes;
  }
}
