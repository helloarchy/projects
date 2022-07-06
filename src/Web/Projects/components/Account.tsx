import { signIn, signOut, useSession } from 'next-auth/react'
import { Button, Card, Text } from '@nextui-org/react'

export default function Account() {
  const { data: session } = useSession()
  return (
    <Card css={{ mw: '400px' }}>
      <Card.Body>
        {session ? (
          <>
            <Text>Signed in as {session.user.email}</Text>
            <Button onClick={() => signOut()}>Sign out</Button>
          </>
        ) : (
          <>
            <Text>Not signed in</Text>
            <Button onClick={() => signIn()}>Sign in</Button>
          </>
        )}
      </Card.Body>
    </Card>
  )
}
