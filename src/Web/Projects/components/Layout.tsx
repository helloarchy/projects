import React, { ReactNode } from 'react'
import Head from 'next/head'

import ThemeSwitch from './ThemeSwitch'
import Navigation from './Navigation'

type Props = {
  children?: ReactNode
  title?: string
}

const Layout = ({ children, title = 'Projects @archy.dev' }: Props) => (
  <>
    <Head>
      <title>{title}</title>
      <meta charSet='utf-8' />
      <meta
        name='viewport'
        content='initial-scale=1.0, width=device-width'
      />
    </Head>
    <header>
      <Navigation />
      <ThemeSwitch />
    </header>
    {children}
    <footer>
      <hr />
      <span>I&apos;m here to stay (Footer)</span>
    </footer>
  </>
)

export default Layout
