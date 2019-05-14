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
    // TODO might move this in a different function.
    if (this.$route.query.SITtoken != undefined) {
      console.log('in created of Redirect view')
      AppSession.updateSession(this.$route.query.SITtoken)
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
                  AppSession.setCategory(response.data.User.Category)
                  AppSession.setSchoolId(response.data.User.SchoolId)
                  AppSession.updateSession(response.data.SITtoken)
                  this.userCategory = AppSession.state.category
                  this.targetURL = this.DIRECTED_PATHS[this.userCategory]
                  if (this.targetURL != undefined) {
                    this.$router.push(this.targetURL)
                  }
                  else {
                    //error
                    console.log(`No target URL is found for the category: ${this.userCategory}`)
                  }
                })
                .catch(error => {
                  //Indicate an error. Server might be down so just logout the user.
                  // delete token to logout the user.
                  // Redirect user to error page.
                  console.log('Was the getcontextid not succesful')
                  console.log('error')
                  console.log(error)
                })
    }
    else {
      console.log('token is undefined.')
      //
      // display error message.
    }
  }
  
}
</script>