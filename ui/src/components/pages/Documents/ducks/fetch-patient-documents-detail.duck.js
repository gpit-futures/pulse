import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

export const FETCH_PATIENT_DOCUMENTS_DETAIL_REQUEST = 'FETCH_PATIENT_DOCUMENTS_DETAIL_REQUEST';
export const FETCH_PATIENT_DOCUMENTS_DETAIL_SUCCESS = 'FETCH_PATIENT_DOCUMENTS_DETAIL_SUCCESS';
export const FETCH_PATIENT_DOCUMENTS_DETAIL_FAILURE = 'FETCH_PATIENT_DOCUMENTS_DETAIL_FAILURE';

export const fetchPatientDocumentsDetailRequest = createAction(FETCH_PATIENT_DOCUMENTS_DETAIL_REQUEST);
export const fetchPatientDocumentsDetailSuccess = createAction(FETCH_PATIENT_DOCUMENTS_DETAIL_SUCCESS);
export const fetchPatientDocumentsDetailFailure = createAction(FETCH_PATIENT_DOCUMENTS_DETAIL_FAILURE);

export const fetchPatientDocumentsDetailEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_DOCUMENTS_DETAIL_REQUEST)
    .mergeMap(({ payload }) =>

      ajax.getJSON(`/api/documents/patient/${payload.userId}/${payload.sourceId}`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map(response => fetchPatientDocumentsDetailSuccess({
          userId: payload.userId,
          documentsDetail: response,
          token: response.token,
        }))
    );

export default function reducer(documentsDetail = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_DOCUMENTS_DETAIL_SUCCESS:
      return _.set(action.payload.userId, action.payload.documentsDetail, documentsDetail);
    default:
      return documentsDetail;
  }
}