<template>
  <v-container>
    <v-layout>
      <v-flex row>
        <v-card center row>
          <v-toolbar card prominent>
            <v-toolbar-title class="body-2">User Profile</v-toolbar-title>
            <template v-slot:extension>
              <v-tabs v-model="tab" align-with-title class="body-2">
                <v-tabs-slider color="yellow"></v-tabs-slider>

                <v-tab>Profile</v-tab>
                <v-tab v-if="isOwnUser">Edit</v-tab>
              </v-tabs>
            </template>
          </v-toolbar>
          <v-divider></v-divider>
          <v-tabs-items v-model="tab">
            <v-tab-item>
              <v-card flat>
                <v-card-text>
                  <p>{{username}}</p>
                  <p>{{name}}</p>
                  <p>Member since: {{memberSince}}</p>
                  <p>Age: {{age}}</p>
                </v-card-text>
              </v-card>
            </v-tab-item>
            <v-tab-item v-if="isOwnUser">
              <v-card flat>
                <!--TODO add control for logging telemetry!-->
                <v-form>
                  
                <v-container>
                  <v-layout row wrap>
                    <v-flex>
                      <v-text-field v-model="newFirstName" label="First name" required></v-text-field>
                    </v-flex>

                    <v-flex>
                      <v-text-field v-model="newMiddleName" label="Middle name" required></v-text-field>
                    </v-flex>

                    <v-flex>
                      <v-text-field v-model="newLastName" label="Last name" required></v-text-field>
                    </v-flex>
                    <v-divider></v-divider>
                    
                  </v-layout>
                  <v-layout>
                    <v-checkbox v-model="allowTelemetry" :label="`Allow telemetric data recording.`"></v-checkbox>
                  </v-layout>
                  <v-layout>
                    <v-btn>Enter</v-btn>
                  </v-layout>
                  
                </v-container>
                </v-form>
              </v-card>
            </v-tab-item>
          </v-tabs-items>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>


<script>
import AppSession from '@/services/AppSession'
export default {
  name: "Profile",
  data() {
    return {
      tab: null,
      username: "",
      firstName: "",
      middleName: "",
      lastName: "",
      memberSince: "date",
      age: "age",
      newFirstName: "New",
      newMiddleName: "new",
      newLastName: "New",
      viewingUserId: AppSession.state.userId,
      allowTelemetry: false
    };
  },
  methods: {
    async getUserInfo(profileUserId) {
      console.log(`Id of get user info is ${profileUserId}`)
      await this.axios.get(`${this.$hostname}UserManager/GetPublicUserInfoWithId?id=${profileUserId}`, 
                     {headers: {'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${AppSession.state.token}`}})
                .then(response => {
                    // Get user userid.
                    AppSession.updateSession(response.data.SITtoken)
                    this.username = response.data.User.UserName
                    this.firstName = response.data.User.FirstName
                    this.lastName = response.data.User.LastName
                    console.log('About to finish getUserInfo')
                })
                .catch(error => {
                  //Indicate an error. Server might be down so just logout the user.
                  // delete token to logout the user.
                  // Redirect user to error page.
                })
    },
    synchToInputs() {
      console.log('This is the first name' + this.firstName)
      this.newFirstName = this.firstName
      // this.newMiddleName = this.middleName
      this.newLastName = this.lastName
      
    }
  },
  computed: {
    name() {
      return this.firstName + " " + this.lastName;
    },
    isOwnUser() {
      if (this.$route != undefined) {
        return parseInt(this.viewingUserId) === parseInt(this.$route.params.id)
      }
      return false;
    }
  },
  watch: {
    '$route' (to,from) {
      if(to.params.id != undefined) {
        this.getUserInfo(to.params.id).then(() =>  {
          console.log('getUserInfo is complete in watch')
          if (this.isOwnUser()) {
            this.synchToInputs()
          }
        }).catch((error)=> {

          })
      }
    }
  },
  created() {
    console.log("In createed of Profile.vue")
    this.getUserInfo(this.$route.params.id)
        .then(() => {
          if (this.isOwnUser) {
            this.synchToInputs()
          }
        }).catch((error)=> {

          })
  }
};
</script>


