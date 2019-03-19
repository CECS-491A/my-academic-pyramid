<template>
  <div id="app">
    <img src="./assets/SITLogo.jpg" height="180" width="360">
    <router-view/>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld'

export default {
  name: 'App',
  components: {
    HelloWorld
  },
  data () {
    return {
      //
    }
  }
}
</script>
