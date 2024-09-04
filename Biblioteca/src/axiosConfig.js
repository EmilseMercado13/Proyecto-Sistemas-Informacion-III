import axios from 'axios';

axios.get('https://localhost:7066/swagger/v1/swagger.json')
  .then(response => {
    console.log('API Schema:', response.data);
  })
  .catch(error => {
    console.error('Error fetching API schema:', error);
  });
