import _ from "lodash/fp";
import { Observable } from 'rxjs';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';
import { convertToFhir } from "../utils/patient-fhir-helper"

import { usersUrls } from '../config/server-urls.constants';

export const FETCH_PATIENT_BANNER_REQUEST = "FETCH_PATIENT_BANNER_REQUEST";
export const FETCH_PATIENT_BANNER_SUCCESS = "FETCH_PATIENT_BANNER_SUCCESS";
export const FETCH_PATIENT_BANNER_ERROR = "FETCH_PATIENT_BANNER_ERROR";

export const fetchPatientBannerRequest = createAction(FETCH_PATIENT_BANNER_REQUEST);
export const fetchPatientBannerSuccess = createAction(FETCH_PATIENT_BANNER_SUCCESS);
export const fetchPatientBannerError = createAction(FETCH_PATIENT_BANNER_ERROR);

export const fetchPatientBannerEpic = (action, store) =>
    action.ofType(FETCH_PATIENT_BANNER_REQUEST)
        .mergeMap(({ payload }) =>
            ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/banner`, {
                Cookie: store.getState().credentials.cookie, Authorization: 'Bearer ' + store.getState().tokens.access_token
              })
                .map(response => fetchPatientBannerSuccess({ userId: payload.userId, banner: response }))
                .catch(error => Observable.of(fetchPatientBannerError(error)))
        );

export default function reducer(patientBanner = {}, action) {
    switch (action.type) {
        case FETCH_PATIENT_BANNER_SUCCESS:
            const { payload } = action;

            // fire patient context changed event
            window.isElectron = function () { return 'Bridge' in window; };
            if (isElectron()) {
                window.Bridge.setPatientContext(convertToFhir(payload.banner));
                console.log('called setPatientContext with : ' + convertToFhir(payload.banner));
            } else {
                console.log('did not load preload script - App not running in an Electron Context')
            }

            return _.set(payload.userId, payload.banner, patientBanner);
        default:
            return patientBanner;
    }
}