import Link from 'next/link'
import Layout from '../components/Layout'
import { signIn, signOut, useSession } from 'next-auth/react'
import { Button } from '@nextui-org/react'

const IndexPage = () => {

  const { data: session } = useSession()

  return (
    <Layout title='Home | Next.js + TypeScript Example'>
      <h1>Hello Next.js 👋</h1>
      <p>
        <Link href='/about'>
          <a>About</a>
        </Link>
      </p>

      {session
        ? (
          <>
            <p>Signed in</p>
            <Button onClick={() => signOut()}>Sign out</Button>
          </>
        )
        : (
          <>
            <p>Not signed in</p>
            <Button onClick={() => signIn()}>Sign in</Button>
          </>
        )
      }

    </Layout>
  )
}

export default IndexPage
