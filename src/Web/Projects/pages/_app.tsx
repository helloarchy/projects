import type { AppProps } from 'next/app'
import { SessionProvider } from 'next-auth/react'
import { createTheme, NextUIProvider } from '@nextui-org/react'
import { ThemeProvider as NextThemesProvider } from 'next-themes'

import '../styles/globals.css'
import darkTheme from '../themes/dark'
import lightTheme from '../themes/light'


const theme = createTheme(darkTheme)

function MyApp({
 Component,
 pageProps: { session, ...pageProps },
}: AppProps) {
  return (
    <SessionProvider session={session}>
      <NextThemesProvider
        defaultTheme='system'
        attribute="class"
        value={{
          light: lightTheme.className!,
          dark: darkTheme.className!
        }}
      >
        <NextUIProvider theme={theme}>
          <Component {...pageProps} />
        </NextUIProvider>
      </NextThemesProvider>
    </SessionProvider>
  )
}

export default MyApp
