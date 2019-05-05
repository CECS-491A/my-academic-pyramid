<template>
<v-dialog max-width="400">
	<v-badge color="red"
	 v-model="newMessage"
	 slot="activator"
	 >
        <template v-slot:badge>
          <span>!</span>
        </template>
	
			<v-icon 
        large color="grey"
		>mail</v-icon>
			
	</v-badge>
			 <v-list two-line>
                    <v-divider></v-divider>
                    <v-list-tile
                      v-for="item in conversations"
                      :key="item.Id"
                      :color="selectedConversationId!= item.Id ? 'black' : 'blue'" 
                      @click=""
                    >
                      <v-list-tile-content>
                        <v-list-tile-title>
                          {{ item.ContactUsername }}</v-list-tile-title>
                        <v-icon :color="item.HasNewMessage && selectedConversationId!= item.Id ? 'red' : 'grey'">chat_bubble</v-icon>
                        <v-list-tile-sub-title>{{item.CreatedDate}}</v-list-tile-sub-title>
                      </v-list-tile-content>

                      <!-- Create an floating window containing a button to delete conversation -->
                      <v-list-tile-action>
                        <v-menu
                        max-height="100px"
                         bottom right>
                          <template v-slot:activator="{ on }">
                            <v-btn dark icon v-on="on" color="black">
                              <v-icon>more_vert</v-icon>
                            </v-btn>
                          </template>
                          <v-card >
                            <v-list>
                              <v-list-tile avatar>
                                <v-list-tile-avatar>
                                  <img src="https://cdn.vuetifyjs.com/images/john.jpg" alt="John">
                                </v-list-tile-avatar>

                                <v-list-tile-content>
                                  <v-list-tile-title
                                 >{{item.ContactUsername}}</v-list-tile-title>
                                  <v-list-tile-sub-title></v-list-tile-sub-title>
                                </v-list-tile-content>
       
                               </v-list-tile>
                            </v-list>
                          </v-card>
                        </v-menu>
                      </v-list-tile-action>
                    </v-list-tile>
                  </v-list>
</v-dialog>
</template>

<script>
export default {
	data() {
    return {
      conversations: [],
      newMessageDialog: false,
      newMessage: {
        contactUsername: "",
        messageContent: ""
	  },
	}
	},
	 created() {
	  this.getAllConversations()
	 },
	methods:{
		  async getAllConversations() { // Method to get all conversation from back end
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "GET",
        crossDomain: true,
        url: this.$hostname + "messenger/GetAllConversation"
      })
        .then(response => { // Populate the conversations data with response data 
          this.conversations = response.data.conversations;
          //sessionStorage.SITtoken = response.data.SITtoken
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },
	}


	  
}
</script>

  <style scoped>
  .miniBox{
    background:white;
    border: 1px so rgba(100,100,100,.4);
    border-radius: 0 0 2px 2px;
    box-shadow: 0 3px 8px rgba (0, 0, 0, .25);
    color: #1d2129;
    overflow: visible;
    position: absolute;
    top: 38px;
    width: 430px;
    z-index: -1;
    
  }
  </style>
