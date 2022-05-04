import NextAuth from "next-auth"
import IdentityServer4 from "next-auth/providers/identity-server4";

export default NextAuth({
    // Configure one or more authentication providers
    providers: [
        IdentityServer4({
            id: "identity-server4",
            name: "IdentityServer4",
            issuer:  process.env.IDENTITY_SERVER_ISSUER,
            clientId: process.env.IDENTITY_SERVER_CLIENT_ID,
            clientSecret: process.env.IDENTITY_SERVER_CLIENT_SECRET
        })
    ],
    callbacks: {
        session({ session, token, user }) {
            return session // The return type will match the one returned in `useSession()`
        },
    },
});