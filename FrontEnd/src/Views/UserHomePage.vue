<template>
  <div class="home">
    <h3>Hello {{ username }}</h3>
  </div>
</template>

<script>
//import axios from 'axios'
export default {
  name: "UserHomePage",
  data () {
    return {
      username: "",
      userid: ""
    }
  },

  created() {
      this.userid = sessionStorage.SITuserid
      // TODO set header
      this.axios
          .get(`${this.$hostname}UserManager/GetUserInfoWithId?id=${sessionStorage.SITuserid}`, 
                {headers: {'Accept': 'application/json',
                           'Content-Type': 'application/json',
                           'Authorization': 'Bearer ' + sessionStorage.SITtoken}})
          .then((response) => {
              this.username = response.data.User.UserName
              sessionStorage.SITtoken = response.data.SITtoken
          })
          .catch((error) => {
              // display error
          });
  }
  
}
</script>