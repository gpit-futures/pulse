import { Observable } from 'rxjs';
import { createAction } from 'redux-actions';

export const SET_TOKENS_START = 'SET_TOKENS_START';
export const SET_TOKENS_SUCCESS = 'SET_TOKENS_SUCCESS';
export const SET_TOKENS_FAILURE = 'SET_TOKENS_FAILURE';

export const setTokensStart = createAction(SET_TOKENS_START);
export const setTokensSuccess = createAction(SET_TOKENS_SUCCESS);
export const setTokensFailure = createAction(SET_TOKENS_FAILURE);

export const setTokensEpic = (action$, store) =>
  action$.ofType(SET_TOKENS_START)
    .map(action => setTokensSuccess(action.payload))
    .catch(error => Observable.of(setTokensFailure(error)))

export default function reducer(tokens = {}, action) {
  switch (action.type) {
    case SET_TOKENS_SUCCESS:
      return action.payload;
    default:
      return tokens;
  }
}
