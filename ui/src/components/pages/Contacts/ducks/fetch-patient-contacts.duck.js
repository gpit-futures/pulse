import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientContactsDetailRequest } from './fetch-patient-contacts-detail.duck';
import { hasTokenInResponse } from '../../../../utils/plugin-helpers.utils';

export const FETCH_PATIENT_CONTACTS_REQUEST = 'FETCH_PATIENT_CONTACTS_REQUEST';
export const FETCH_PATIENT_CONTACTS_SUCCESS = 'FETCH_PATIENT_CONTACTS_SUCCESS';
export const FETCH_PATIENT_CONTACTS_FAILURE = 'FETCH_PATIENT_CONTACTS_FAILURE';
export const FETCH_PATIENT_CONTACTS_UPDATE_REQUEST = 'FETCH_PATIENT_CONTACTS_UPDATE_REQUEST';

export const fetchPatientContactsRequest = createAction(FETCH_PATIENT_CONTACTS_REQUEST);
export const fetchPatientContactsSuccess = createAction(FETCH_PATIENT_CONTACTS_SUCCESS);
export const fetchPatientContactsFailure = createAction(FETCH_PATIENT_CONTACTS_FAILURE);
export const fetchPatientContactsUpdateRequest = createAction(FETCH_PATIENT_CONTACTS_UPDATE_REQUEST);

export const fetchPatientContactsEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_CONTACTS_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/contacts`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map((response) => {
          const token = hasTokenInResponse(response);
          return fetchPatientContactsSuccess({
            userId: payload.userId,
            contacts: response,
            token,
          })
        })
    );

export const fetchPatientContactsUpdateEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_CONTACTS_UPDATE_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/contacts`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .flatMap((response) => {
          const userId = payload.userId;
          const sourceId = payload.sourceId;

          return [
            fetchPatientContactsSuccess({ userId, contacts: response }),
            fetchPatientContactsDetailRequest({ userId, sourceId }),
          ]
        })

    );

export default function reducer(patientsContacts = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_CONTACTS_SUCCESS:
      return _.set(action.payload.userId, action.payload.contacts, patientsContacts);
    default:
      return patientsContacts;
  }
}
