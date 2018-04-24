import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientReferralsUpdateRequest } from './fetch-patient-referrals.duck'

export const FETCH_PATIENT_REFERRALS_DETAIL_EDIT_REQUEST = 'FETCH_PATIENT_REFERRALS_DETAIL_EDIT_REQUEST';
export const FETCH_PATIENT_REFERRALS_DETAIL_EDIT_SUCCESS = 'FETCH_PATIENT_REFERRALS_DETAIL_EDIT_SUCCESS';
export const FETCH_PATIENT_REFERRALS_DETAIL_EDIT_FAILURE = 'FETCH_PATIENT_REFERRALS_DETAIL_EDIT_FAILURE';

export const fetchPatientReferralsDetailEditRequest = createAction(FETCH_PATIENT_REFERRALS_DETAIL_EDIT_REQUEST);
export const fetchPatientReferralsDetailEditSuccess = createAction(FETCH_PATIENT_REFERRALS_DETAIL_EDIT_SUCCESS);
export const fetchPatientReferralsDetailEditFailure = createAction(FETCH_PATIENT_REFERRALS_DETAIL_EDIT_FAILURE);

export const fetchPatientReferralsDetailEditEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_REFERRALS_DETAIL_EDIT_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.put(`${usersUrls.PATIENTS_URL}/${payload.userId}/referrals/${payload.sourceId}`, payload, {
        Cookie: store.getState().credentials.cookie,
        'Content-Type': 'application/json',
      })
        .flatMap(({ response }) => {
          const userId = payload.userId;
          const sourceId = payload.sourceId;

          return [
            fetchPatientReferralsDetailEditSuccess(response),
            fetchPatientReferralsUpdateRequest({ userId, sourceId }),
          ];
        })
    );

export default function reducer(referralsDetailEdit = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_REFERRALS_DETAIL_EDIT_SUCCESS:
      return action.payload;
    default:
      return referralsDetailEdit;
  }
}
