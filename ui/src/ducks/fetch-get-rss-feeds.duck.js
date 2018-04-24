import _ from 'lodash/fp';
import { ajax } from 'rxjs/observable/dom/ajax';
import { createAction } from 'redux-actions';

import { getRssFeedsListFromXML } from '../utils/rss-helpers';

export const FETCH_GET_RSS_FEEDS_REQUEST = 'FETCH_GET_RSS_FEEDS_REQUEST';
export const FETCH_GET_RSS_FEEDS_SUCCESS = 'FETCH_GET_RSS_FEEDS_SUCCESS';
export const FETCH_GET_RSS_FEEDS_FAILURE = 'FETCH_GET_RSS_FEEDS_FAILURE';

export const fetchGetRssFeedsRequest = createAction(FETCH_GET_RSS_FEEDS_REQUEST);
export const fetchGetRssFeedsSuccess = createAction(FETCH_GET_RSS_FEEDS_SUCCESS);
export const fetchGetRssFeedsFailure = createAction(FETCH_GET_RSS_FEEDS_FAILURE);

export const fetchGetRssFeedsEpic = (action$, store) =>
  action$.ofType(FETCH_GET_RSS_FEEDS_REQUEST)
    .mergeMap(({ payload }) =>
      ajax({
        url: payload.rssFeedUrl,
        responseType: 'application/rss+xml',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        crossDomain: true,
      })
        .map(response => response.xhr.responseXML)
        .map(xmlDocument => fetchGetRssFeedsSuccess({
          rssFeedName: payload.rssFeedName,
          feeds: getRssFeedsListFromXML(xmlDocument),
        }))
    );


export default function reducer(rssFeeds = {}, action) {
  switch (action.type) {
    case FETCH_GET_RSS_FEEDS_SUCCESS:
      return _.set(action.payload.rssFeedName, action.payload.feeds, rssFeeds);
    default:
      return rssFeeds;
  }
}
