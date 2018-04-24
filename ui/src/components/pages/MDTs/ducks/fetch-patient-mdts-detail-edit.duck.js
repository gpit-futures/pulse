import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientMDTsUpdateRequest } from './fetch-patient-mdts.duck'

export const FETCH_PATIENT_MDTS_DETAIL_EDIT_REQUEST = 'FETCH_PATIENT_MDTS_DETAIL_EDIT_REQUEST';
export const FETCH_PATIENT_MDTS_DETAIL_EDIT_SUCCESS = 'FETCH_PATIENT_MDTS_DETAIL_EDIT_SUCCESS';
export const FETCH_PATIENT_MDTS_DETAIL_EDIT_FAILURE = 'FETCH_PATIENT_MDTS_DETAIL_EDIT_FAILURE';

export const fetchPatientMDTsDetailEditRequest = createAction(FETCH_PATIENT_MDTS_DETAIL_EDIT_REQUEST);
export const fetchPatientMDTsDetailEditSuccess = createAction(FETCH_PATIENT_MDTS_DETAIL_EDIT_SUCCESS);
export const fetchPatientMDTsDetailEditFailure = createAction(FETCH_PATIENT_MDTS_DETAIL_EDIT_FAILURE);

export const fetchPatientMDTsDetailEditEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_MDTS_DETAIL_EDIT_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.put(`${usersUrls.PATIENTS_URL}/${payload.userId}/mdtreports/${payload.sourceId}`, payload, {
        Cookie: store.getState().credentials.cookie,
        'Content-Type': 'application/json',
      })
        .flatMap(({ response }) => {
          const userId = payload.userId;
          const sourceId = payload.sourceId;

          return [
            fetchPatientMDTsDetailEditSuccess(response),
            fetchPatientMDTsUpdateRequest({ userId, sourceId }),
          ];
        })
    );

export default function reducer(mdtsDetailEdit = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_MDTS_DETAIL_EDIT_SUCCESS:
      return action.payload;
    default:
      return mdtsDetailEdit;
  }
}
