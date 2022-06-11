import Document, { Html, Head, Main, NextScript, DocumentContext } from 'next/document'
import { CssBaseline } from '@nextui-org/react'

/**
 * Rendered on server only, used to update the html and body tags for a Page.
 * This overrides the default Document, and is required for Next UI.
 * See: https://nextjs.org/docs/advanced-features/custom-document
 */
class MyDocument extends Document {
  static async getInitialProps(ctx: DocumentContext) {
    const initialProps = await Document.getInitialProps(ctx)
    return {
      ...initialProps,
      styles: [<>{initialProps.styles}</>],
    }
  }

  render() {
    return (
      <Html lang='en'>
        <Head>{CssBaseline.flush()}</Head>
        <body>
        <Main />
        <NextScript />
        </body>
      </Html>
    )
  }
}

export default MyDocument