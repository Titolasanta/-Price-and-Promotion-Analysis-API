// config.js
const config = {
    development: {
      apiUrl: 'http://localhost:5118', // Development API URL
    },

  };
  
  // Get the current environment (either 'development' or 'production')
  const currentEnv = process.env.NODE_ENV || 'development';
  
  // Export the config for the current environment
  export default config[currentEnv];
  