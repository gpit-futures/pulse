const baseUrl = process.env.BASE_URL || "";

export const apiUrls = {
  INITIALISE: baseUrl + '/api/initialise',
  LOGOUT: baseUrl + '/api/logout',
};

export const usersUrls = {
  PATIENTS_URL: baseUrl + '/api/patients',
  USER_ACCOUNT_URL: baseUrl + '/api/user',
  BASIC_PATIENT_SEARCH: baseUrl + '/api/search/patient/table',
  ADVANCED_PATIENT_SEARCH: baseUrl + '/api/patients/advancedSearch',
  PROFILE_APP_PREFERENCES: baseUrl + '/api/application',
  LIST_ORDERS: baseUrl + '/api/terminology/list/order',
  PICTURES: baseUrl + '/api/pictures',
  CLINICAL_QUERY_SEARCH: baseUrl + '/api/patients/querySearch',
  FEEDS: baseUrl + '/api/feeds',
};
