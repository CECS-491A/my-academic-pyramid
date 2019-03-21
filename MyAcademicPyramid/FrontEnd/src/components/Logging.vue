<template>
  <div class="hello">
    <h1>{{ info }}</h1>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  data () {
    return {
      info: null,
    }
  },
  mounted () {
    axios
      .get('http:///api.myacademicpyramid.com/api/logging/geterrors', {
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(response => (this.info = response.data))
      
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
