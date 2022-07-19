import NextAuth from 'next-auth'
import IdentityServer4 from 'next-auth/providers/identity-server4'

export default NextAuth({
  // Configure one or more authentication providers
  providers: [
    IdentityServer4({
      id: 'identity-server',
      name: 'IdentityServer',
      type: 'oauth',
      wellKnown: 'http://localhost:4000/.well-known/openid-configuration',
      authorization: {
        params: {
          scope: 'openid profile scope2 offline_access readProjectApi',
        },
      },
      checks: ['pkce', 'state'],
      idToken: true,
      issuer: process.env.IDENTITY_SERVER_ISSUER,
      clientId: process.env.IDENTITY_SERVER_CLIENT_ID,
      clientSecret: process.env.IDENTITY_SERVER_CLIENT_SECRET,
    }),
  ],
  callbacks: {
    async jwt({ token, account }) {
      // Persist the OAuth access_token to the token right after sign in
      if (account) {
        token.accessToken = account.access_token
      }
      return token
    },
    async session({ session, token, user }) {
      // Send properties to the client, like an access_token from a provider.
      session.accessToken = token.accessToken
      return session
    },
  },
  secret: process.env.IDENTITY_SERVER_CLIENT_SECRET,
})
