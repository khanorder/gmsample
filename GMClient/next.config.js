/**
 * @type {import('next').NextConfig}
 */

import withBundleAnalyzer from '@next/bundle-analyzer';
const bundleAnalyzer = withBundleAnalyzer({ enable: true });

const ContentSecurityPolicy = `
  default-src 'self' https://www.google-analytics.com/g/collect https://googleads.g.doubleclick.net https://apis.google.com/js/api.js;
  base-uri 'self';
  block-all-mixed-content;
  font-src 'self' https: data:;
  form-action 'self';
  frame-ancestors 'self';
  frame-src 'self' https://www.google.com https://googleads.g.doubleclick.net https://tpc.googlesyndication.com https://tower-of-god-45436900.firebaseapp.com https://www.youtube.com https://www.youtube-nocookie.com;
  img-src 'self' data: https://pagead2.googlesyndication.com https://www.google.com https://www.googletagmanager.com https://wcs.naver.com https://i.ytimg.com https://www.facebook.com;
  object-src 'none';
  connect-src 'self' https://www.google-analytics.com https://pagead2.googlesyndication.com https://tpc.googlesyndication.com https://csi.gstatic.com https://identitytoolkit.googleapis.com https://securetoken.googleapis.com/v1/token https://auth.firebase.com http://localhost:4100 ws://localhost:4100;
  script-src 'self' https: https://wcs.naver.net https://www.google-analytics.com https://www.googletagmanager.com https://googleads.g.doubleclick.net https://pagead2.googlesyndication.com https://connect.facebook.net;
  script-src-elem 'self' https: 'unsafe-inline' https://www.googletagmanager.com https://pagead2.googlesyndication.com https://googleads.g.doubleclick.net https://www.google-analytics.com;
  script-src-attr 'none';
  style-src 'self' https: 'unsafe-inline';
  upgrade-insecure-requests
`;

const securityHeaders = [
  {
    key: 'Content-Security-Policy',
    value: ContentSecurityPolicy.replace(/\s{2,}/g, ' ').trim()
  },
  {
    key: 'Cross-Origin-Embedder-Policy',
    value: 'require-corp | credentialless'
  },
  {
    key: 'Cross-Origin-Opener-Policy',
    value: 'same-origin-allow-popups'
  },
  {
    key: 'Cross-Origin-Resource-Policy',
    value: 'same-origin'
  }
];

const nextConfig = {
  /* config options here */
  poweredByHeader: false,
  swcMinify: false,
  webpack(config) {
    config.module.rules.push({
      test: /\.svg$/,
      use: ["@svgr/webpack"]
    });
    return config;
  },

  async headers() {
    const headers = process.env.NODE_ENV == "production"
      ? [
        // {
        //   source: '/:path*',
        //   headers: securityHeaders
        // }
      ]
      : [];
    return headers; 
  },
}

export default 'true' === process.env.ANALYZE ? bundleAnalyzer(nextConfig) : nextConfig;