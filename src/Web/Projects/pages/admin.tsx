import Link from 'next/link'
import { Button, Container, Grid, Text } from '@nextui-org/react'

import Layout from '../components/Layout/Layout'
import Account from '../components/Account'
import { useSession } from 'next-auth/react'

const AdminPage = () => {
  const { data: session } = useSession()

  return (
    <Layout title="Admin | Archy.dev">
      {session ? (
        <>
          <Text h1>Admin</Text>
          <Text>This is the admin page</Text>
          <Link href="/">
            <a>Go home</a>
          </Link>
          <Account />
        </>
      ) : (
        <>
          <Text h1>This is a restricted area </Text>
          <Account />
        </>
      )}
    </Layout>
  )
}

export default AdminPage
