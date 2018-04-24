import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientTopThreeThingsDetailRequest } from './fetch-patient-top-three-things-detail.duck';
import { hasTokenInResponse } from '../../../../utils/plugin-helpers.utils';

export const FETCH_PATIENT_TOP_THREE_THINGS_REQUEST = 'FETCH_PATIENT_TOP_THREE_THINGS_REQUEST';
export const FETCH_PATIENT_TOP_THREE_THINGS_SUCCESS = 'FETCH_PATIENT_TOP_THREE_THINGS_SUCCESS';
export const FETCH_PATIENT_TOP_THREE_THINGS_FAILURE = 'FETCH_PATIENT_TOP_THREE_THINGS_FAILURE';
export const FETCH_PATIENT_TOP_THREE_THINGS_UPDATE_REQUEST = 'FETCH_PATIENT_TOP_THREE_THINGS_UPDATE_REQUEST';

export const fetchPatientTopThreeThingsRequest = createAction(FETCH_PATIENT_TOP_THREE_THINGS_REQUEST);
export const fetchPatientTopThreeThingsSuccess = createAction(FETCH_PATIENT_TOP_THREE_THINGS_SUCCESS);
export const fetchPatientTopThreeThingsFailure = createAction(FETCH_PATIENT_TOP_THREE_THINGS_FAILURE);
export const fetchPatientTopThreeThingsUpdateRequest = createAction(FETCH_PATIENT_TOP_THREE_THINGS_UPDATE_REQUEST);

export const fetchPatientTopThreeThingsEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_TOP_THREE_THINGS_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/top3Things`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map((response) => {
          const token = hasTokenInResponse(response);
          return fetchPatientTopThreeThingsSuccess({
            userId: payload.userId,
            topThreeThings: response,
            token,
          })
        })
    );

export const fetchPatientTopThreeThingsUpdateEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_TOP_THREE_THINGS_UPDATE_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/top3Things`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .flatMap((response) => {
          const userId = payload.userId;
          const sourceId = payload.sourceId;

          return [
            fetchPatientTopThreeThingsSuccess({ userId, topThreeThings: response }),
            fetchPatientTopThreeThingsDetailRequest({ userId, sourceId }),
          ]
        })
    );

export default function reducer(patientsTopThreeThings = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_TOP_THREE_THINGS_SUCCESS:
      return _.set(action.payload.userId, action.payload.topThreeThings, patientsTopThreeThings);
    default:
      return patientsTopThreeThings;
  }
}
