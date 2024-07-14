export const environment = {
  production: false,
  apiUrl: 'http://localhost:5461',
  endpoints: {
    auth: {
      register: '/api/Auth/Register',
      login: '/api/Auth/Login'
    },
  }
};