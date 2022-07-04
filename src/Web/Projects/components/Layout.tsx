import { Container } from '@nextui-org/react'
import React, { ReactNode } from 'react'
import Head from 'next/head'

import ThemeSwitch from './ThemeSwitch'
import Navigation from './Navigation'
import Footer from './Footer'

type Props = {
  children?: ReactNode
  title?: string
}

const Layout = ({ children, title = 'Projects @archy.dev' }: Props) => {
  return (
    <Container>
      <Head>
        <title>{title}</title>
        <meta charSet="utf-8" />
        <meta name="viewport" content="initial-scale=1.0, width=device-width" />
      </Head>
      <header>
        <Navigation />
        <ThemeSwitch />
      </header>
      {children}
      <Footer />
    </Container>
  )
}

export default Layout
