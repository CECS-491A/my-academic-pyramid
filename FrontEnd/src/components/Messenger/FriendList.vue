<template>
  <!-- Friend list component that hold friends of user  -->
  <v-app id="friendList">
    <v-card>
      <v-toolbar 
      color="teal" 
      dark>
        <v-spacer></v-spacer>
        <!-- Create add friend button -->
        <v-btn
          id="addFriendButton"
          fab
          small
          color="red"
          top
          right
          absolute
          @click="addFriendDialog = !addFriendDialog"
        >
          <v-icon>add</v-icon>
        </v-btn>
      </v-toolbar>
      <!-- Create a friendlist but ability to send a new message  -->
      <v-list subheader>
        <v-list-tile
          v-for="friend in friendList"
          :key="friend.FriendId"
          @click="sendNewMessage(friend.FriendUsername)"
        >
          <v-list-tile-content>
            <v-list-tile-title>{{friend.FriendUsername}}</v-list-tile-title>
          </v-list-tile-content>

          <!-- Create a drop down menu to delete a friend from list -->
          <v-list-tile-action>
            <v-menu bottom left
            max-height="100px">
              <template v-slot:activator="{ on }">
                <v-btn 
                dark 
                icon
                v-on="on" 
                color="black">
                  <v-icon>more_vert</v-icon>
                </v-btn>
              </template>
              <v-card>
                <v-list>
                  <v-list-tile avatar>
                    <v-list-tile-avatar>
                      <img src="https://cdn.vuetifyjs.com/images/john.jpg" alt="John">
                    </v-list-tile-avatar>

                    <v-list-tile-content>
                      <v-list-tile-title>{{friendList.FriendUsername}}</v-list-tile-title>
                    </v-list-tile-content>

                    <!-- Create button to delete friend -->
                    <v-list-tile-action>
                      <v-btn icon @click="deleteFriend(friend.FriendId)">
                        <v-icon>delete</v-icon>
                      </v-btn>
                    </v-list-tile-action>
                  </v-list-tile>
                </v-list>
                <v-divider></v-divider>
              </v-card>
            </v-menu>
          </v-list-tile-action>
        </v-list-tile>
      </v-list>
      <v-divider></v-divider>
    </v-card>

    <!-- Create a dialog to add new friend -->
    <v-dialog v-model="addFriendDialog" max-width="500px">
      <v-card>
        <form ref="form">
          <v-card-text>
            <!-- Friend's username text field -->
            <v-text-field
              label="Friend's username"
              v-model="addFriendUsername"
              :error-messages="usernameErrors"
              required
              @input="$v.addFriendUsername.$touch()"
              @blur="$v.addFriendUsername.$touch()"
            ></v-text-field>
          </v-card-text>
        </form>
        <v-card-actions>
          <v-spacer></v-spacer>
          <!-- Add Friendbutton -->
          <v-btn flat color="primary" @click="addFriend">Add</v-btn>
        </v-card-actions>
      </v-card>

      <v-alert :value="error" type="error" transition="scale-transition">{{error}}</v-alert>
    </v-dialog>
  </v-app>
</template>
<script>
import { validationMixin } from "vuelidate";
import { required, email } from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],
  validations: {
    addFriendUsername: { required, email }
  },

  data() {
    return {
      friendList: {},

      friendTO: {
        Id: "",
        username: ""
      },

      addFriendUsername: "",
      addFriendDialog: false,
      error: ""
    };
  },
  computed: {
    usernameErrors() {
      const errors = [];

      if (!this.$v.addFriendUsername.$dirty) return errors;

      !this.$v.addFriendUsername.email && errors.push("Must be valid e-mail");

      !this.$v.addFriendUsername.required && errors.push("E-mail is required");

      return errors;
    }
  },
  watch: {
    addFriendDialog() {
      this.addFriendUsername = "";
      this.error = "";
    }
  },

  created() {
    this.loadFriendList();
  },
  methods: {
    async loadFriendList() {
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "GET",
        crossDomain: true,
        url: this.$hostname + "messenger/GetFriendList"
      })
        .then(response => {
          this.friendList = response.data.friendList;
          // sessionStorage.SITtoken = response.data.SITtoken
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },

    async addFriend() {
      if (this.addFriendUsername) {
        await this.axios({
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + sessionStorage.SITtoken
          },
          method: "POST",
          crossDomain: true,
          url:
            this.$hostname +
            "messenger/AddFriend?addedUsername=" +
            this.addFriendUsername
        })
          .then(response => {
            //  sessionStorage.SITtoken = response.data.SITtoken,
            this.addFriendDialog = false;

            this.friendList.push(response.data.friend);
          })
          .catch(err => {
            /* eslint no-console: "off" */
            this.error = err.response.data;
            console.log(err);
          });
      }
    },

    sendNewMessage(friendUsername) {
      this.$eventBus.$emit("SendMessageFromFriendList", friendUsername);
    },

    async deleteFriend(friendId) {
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "DELETE",
        crossDomain: true,
        url:
          this.$hostname + "messenger/RemoveFriendFromList?friendId=" + friendId
      })
        .then(() => {
          //sessionStorage.SITtoken = response.data.SITtoken,
          this.loadFriendList();

          //this.friendList.push({FriendUserName: this.addFriendUsername});
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    }
  }
};
</script>
