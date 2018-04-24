import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { usersUrls } from '../../../../config/server-urls.constants'
import { fetchPatientEventsDetailRequest } from './fetch-patient-events-detail.duck';
import { hasTokenInResponse } from '../../../../utils/plugin-helpers.utils';


export const FETCH_PATIENT_EVENTS_REQUEST = 'FETCH_PATIENT_EVENTS_REQUEST';
export const FETCH_PATIENT_EVENTS_SUCCESS = 'FETCH_PATIENT_EVENTS_SUCCESS';
export const FETCH_PATIENT_EVENTS_FAILURE = 'FETCH_PATIENT_EVENTS_FAILURE';
export const FETCH_PATIENT_EVENTS_UPDATE_REQUEST = 'FETCH_PATIENT_EVENTS_UPDATE_REQUEST';

export const fetchPatientEventsRequest = createAction(FETCH_PATIENT_EVENTS_REQUEST);
export const fetchPatientEventsSuccess = createAction(FETCH_PATIENT_EVENTS_SUCCESS);
export const fetchPatientEventsFailure = createAction(FETCH_PATIENT_EVENTS_FAILURE);
export const fetchPatientEventsUpdateRequest = createAction(FETCH_PATIENT_EVENTS_UPDATE_REQUEST);

export const fetchPatientEventsEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_EVENTS_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/events`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .map((response) => {
          const token = hasTokenInResponse(response);
          return fetchPatientEventsSuccess({
            userId: payload.userId,
            events: response,
            token,
          })
        })
    );

export const fetchPatientEventsUpdateEpic = (action$, store) =>
  action$.ofType(FETCH_PATIENT_EVENTS_UPDATE_REQUEST)
    .mergeMap(({ payload }) =>
      ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/events`, {
        headers: { Cookie: store.getState().credentials.cookie },
      })
        .flatMap((response) => {
          const userId = payload.userId;
          const sourceId = payload.sourceId;

          return [
            fetchPatientEventsSuccess({ userId, events: response }),
            fetchPatientEventsDetailRequest({ userId, sourceId }),
          ]
        })
    );

export default function reducer(patientsEvents = {}, action) {
  switch (action.type) {
    case FETCH_PATIENT_EVENTS_SUCCESS:
      return _.set(action.payload.userId, action.payload.events, patientsEvents);
    default:
      return patientsEvents;
  }
}
