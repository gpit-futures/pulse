import _ from "lodash/fp";
import { Observable } from 'rxjs';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';
import { DwClientConnector } from "dw-client-connector";
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
            ajax.getJSON(`${usersUrls.PATIENTS_URL}/${payload.userId}/banner`)
                .map(response => fetchPatientBannerSuccess({ userId: payload.userId, banner: response }))
                .catch(error => Observable.of(fetchPatientBannerError(error)))
        );

export default function reducer(patientBanner = {}, action) {
    switch (action.type) {
        case FETCH_PATIENT_BANNER_SUCCESS:
            const { payload } = action;
            DwClientConnector.publish({ name: "patient-context:changed", data: convertToFhir(payload.banner) });
            return _.set(payload.userId, payload.banner, patientBanner);
        default:
            return patientBanner;
    }
}