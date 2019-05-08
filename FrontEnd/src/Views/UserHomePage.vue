<template>
  <div class="home">
    <h3>Hello {{ username }}</h3>
  </div>
</template>

<script>
//import axios from 'axios'
import AppSession from "@/services/AppSession"

export default {
  name: "UserHomePage",
  data () {
    return {
      username: "",
      userid: ""
    }
  },

  created() {
      this.userid = AppSession.state.userId
      // TODO set header
      console.log(`In created of UserHomePage: ${AppSession.state.token}`)
      this.axios
          .get(`${this.$hostname}UserManager/GetUserInfoWithId?id=${AppSession.state.userId}`, 
                {headers: {'Accept': 'application/json',
                           'Content-Type': 'application/json',
                           'Authorization': 'Bearer ' + AppSession.state.token}})
          .then((response) => {
              this.username = response.data.User.UserName
              console.log(`Updating session in UserHomPage. New token ${response.data.SITtoken}`)
              AppSession.updateSession(response.data.SITtoken)
          })
          .catch((error) => {
              // display error
          });
  }
  
}
</script>
<style>
body {
  /* background-image: url('../assets/csulb_pyramid.jpg'); */
  width: 2000px;
  margin: auto;
}
</style>
