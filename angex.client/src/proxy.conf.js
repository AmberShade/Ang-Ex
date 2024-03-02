const PROXY_CONFIG = [
  {
    context: [
      "/tasks"
    ],
    target: "https://localhost:7044",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
