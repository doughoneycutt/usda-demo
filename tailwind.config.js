/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './src/**/*.{razor,cshtml,html}'
  ],
  theme: {
    extend: {
      colors: {
        field: {
          50: '#f0fdf4',
          500: '#16a34a',
          700: '#15803d'
        }
      }
    }
  },
  plugins: []
};
