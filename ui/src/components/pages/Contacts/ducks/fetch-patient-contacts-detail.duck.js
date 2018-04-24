import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'

export const FETCH_PATIENT_CONTACTS_DETAIL_REQUEST = 'FETCH_PATIENT_CONTACTS_DETAIL_REQUEST';
export const FETCH_PATIENT_CONTACTS_DETAIL_SUCCESS = 'FETCH_PATIENT_CONTACTS_DETAIL_SUCCESS';
export const FETCH_PATIENT_CONTACTS_DETAIL_FAILURE = 'FETCH_PATIENT_CONTACTS_DETAIL_FAILURE';

export const fetchPatientContactsDetailRequest = createAction(FETCH_PATIENT_CONTACTS_DETAIL_REQUEST);
export const fetchPatientContactsDetailSuccess = createAction(FETCH_PATIENT_CONTACTS_DETAIL_SUCCESS);
export const fetchPatientContactsDetailFailure = createAction(FETCH_PATIENT_CONTACTS_DETAIL_FAILURE);

export const fetchPatientContactsDetailEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_CONTACTS_DETAIL_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/contacts/${payload.sourceId}`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map(response => fetchPatientContactsDetailSuccess({
          userId: payload.userId,
          contactsDetail: response,
          token: response.token,
        }))
    );

export default function reducer(contactsDetail = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_CONTACTS_DETAIL_SUCCESS:
      return _.set(action.payload.userId, action.payload.contactsDetail, contactsDetail);
    default:
      return contactsDetail;
  }
}