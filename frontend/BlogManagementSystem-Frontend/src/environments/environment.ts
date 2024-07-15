export const environment = {
  production: false,
  apiUrl: 'http://localhost:5461',
  endpoints: {
    auth: {
      register: '/api/Auth/Register',
      login: '/api/Auth/Login'
    },
    blogPosts: {
      getList: '/api/BlogPosts/get',
      getById: '/api/BlogPosts/get/',
      create: '/api/BlogPosts/create',
      update: '/api/BlogPosts/update',
      delete: '/api/BlogPosts/delete'
    },
    comments: {
      create: '/api/Comments/create',
      delete: '/api/Comments/delete'
    }
  }
};