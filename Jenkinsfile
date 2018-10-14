node {
  stage('Pull from SCM') {
      git credentialsId: 'GitHubToken', url: 'https://github.com/monofor/apk-net.git'
  }
  stage('Build') {
      dir('./src') {
          sh 'dotnet build'
      }
  }
  stage('Test') {
      dir('./test/ApkNet.ApkReader.Test') {
          sh 'dotnet test'
      }
  }
}