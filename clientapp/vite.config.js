import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'


export default defineConfig({
  plugins: [vue()],
  base: '/notes-app/' // 👈 important for correct routing on GitHub Pages
})

// export default defineConfig({
//   plugins: [vue()],
// server: {
//   proxy: {
//     '/api': {
//       target: 'https://localhost:7200', // Matches your .NET backend
//       changeOrigin: true,
//       secure: false, // Use false for self-signed certs
//     }
//   }
// }
// ,
// })
