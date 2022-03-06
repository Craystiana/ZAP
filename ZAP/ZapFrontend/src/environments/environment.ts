// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false
};

export const API_URL = 'https://10.0.2.2:44356';
export const LOGIN_URL = '/user/login'; 
export const REGISTER_URL = '/user/register';
export const REGISTER_ADMIN_URL = '/user/registerAdmin';
export const PROFILE_URL = '/user/profile';
export const CUSTOMER_URL = '/customer/all';
export const CUSTOMER_DETAILS_URL = '/customer/detail?customerId=';
export const CUSTOMER_BLACKLIST_URL = '/customer/blacklist?customerId=';
export const CAR_DETAIL_URL = '/car/detail?carId=';
export const CAR_DATA_URL = '/car/data';
export const CAR_EDIT_URL = '/car/edit';
export const CAR_LIST_URL = '/car/list';
export const CAR_DELETE_URL = '/car/delete?carId=';
export const RESERVATION_ADD_URL = '/reservation/add';
export const RESERVATION_REMOVE_URL = '/reservation/remove';
export const RESERVATION_USER_LIST = '/reservation/userList?userId=';
export const RESERVATION_CAR_LIST = '/reservation/carList?carId=';

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
