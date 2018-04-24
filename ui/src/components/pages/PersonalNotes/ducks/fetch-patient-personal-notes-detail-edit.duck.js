import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientPersonalNotesUpdateRequest } from './fetch-patient-personal-notes.duck'

export const FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_REQUEST = 'FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_REQUEST';
export const FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_SUCCESS = 'FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_SUCCESS';
export const FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_FAILURE = 'FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_FAILURE';

export const fetchPatientPersonalNotesDetailEditRequest = createAction(FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_REQUEST);
export const fetchPatientPersonalNotesDetailEditSuccess = createAction(FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_SUCCESS);
export const fetchPatientPersonalNotesDetailEditFailure = createAction(FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_FAILURE);

export const fetchPatientPersonalNotesDetailEditEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.put(`${usersUrls.PATIENTS_URL}/${payload.userId}/personalnotes/${payload.sourceId}`, payload, {
        Cookie: store.getState().credentials.cookie,
        'Content-Type': 'application/json',
      })
        .flatMap(({ response }) => {
          const userId = payload.userId;
          const sourceId = payload.sourceId;

          return [
            fetchPatientPersonalNotesDetailEditSuccess(response),
            fetchPatientPersonalNotesUpdateRequest({ userId, sourceId }),
          ];
        })
    );

export default function reducer(personalNotesDetailEdit = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_PERSONAL_NOTES_DETAIL_EDIT_SUCCESS:
      return action.payload;
    default:
      return personalNotesDetailEdit;
  }
}