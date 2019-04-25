<template>
  <div class="home">
    <h1>{{ redirectMsg }}</h1>
  </div>
</template>

<script>
//import axios from 'axios'
export default {
  data () {
    return {
      redirectMsg: "Redirecting",
      DIRECTED_PATHS: {
        NewUser: '/UserRegistration',
        Student: '/UserHomePage',
        NonStudent: '/other'
      },
      userCategory: "",
      targetURL: ""
    }
  },
  created() {
    if (this.$route.query.SITtoken != undefined) {
      sessionStorage.SITtoken = this.$route.query.SITtoken
        
      console.log("In creation")
      // try to get userinfo
      this.axios.get(`${this.$hostname}UserManager/GetContextId`, 
                     {headers: {'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${sessionStorage.SITtoken}`}})
                .then(response => {
                    // Get user userid.
                    sessionStorage.SITuserid = response.data.userid
                    sessionStorage.SITtoken = response.data.SITtoken
                    return this.axios.get(
                      `${this.$hostname}UserManager/GetUserInfoWithId?id=${sessionStorage.SITuserid}`, 
                      {headers: {'Accept': 'application/json',
                                  'Content-Type': 'application/json',
                                  'Authorization': `Bearer ${sessionStorage.SITtoken}`}}
                    )
                })
                .then(response => {
                  this.userCategory = response.data.User.Category
                  sessionStorage.SITtoken = response.data.SITtoken
                  this.targetURL = this.DIRECTED_PATHS[this.userCategory]
                  if (this.targetURL != undefined) {
                    this.$router.push(this.targetURL)
                  }
                  else {
                    //error
                  }
                })
                .catch(error => {
                  //Indicate an error. Server might be down so just logout the user.
                  // delete token to logout the user.
                  // Redirect user to error page.
                })
    }
    else {
      //
      // display error message.
    }
  }
  
}
</script>