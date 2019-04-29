<template>
  <div class="home">
    <h1>{{ redirectMsg }}</h1>
  </div>
</template>

<script>
//import axios from 'axios'
import AppSession from "@/services/AppSession"

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
      AppSession.updateSession(this.$route.query.SITtoken)
        
      console.log("In creation of Redirect")
      console.log(AppSession.state.token)
      // try to get userinfo
      this.axios.get(`${this.$hostname}UserManager/GetContextId`, 
                     {headers: {'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${AppSession.state.token}`}})
                .then(response => {
                    // Get user userid.
                    AppSession.updateSession(response.data.SITtoken)
                    AppSession.setUserId(response.data.userid)
                    console.log(AppSession.state.userId)

                    return this.axios.get(
                      `${this.$hostname}UserManager/GetUserInfoWithId?id=${AppSession.state.userId}`, 
                      {headers: {'Accept': 'application/json',
                                  'Content-Type': 'application/json',
                                  'Authorization': `Bearer ${AppSession.state.token}`}}
                    )
                })
                .then(response => {
                  this.userCategory = response.data.User.Category
                  AppSession.updateSession(response.data.SITtoken)
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