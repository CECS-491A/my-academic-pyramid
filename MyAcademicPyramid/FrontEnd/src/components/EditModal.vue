<script>
  export default {
	name: 'EditModal',
	// props:{
	// 	UserName:{
  //     type: String,
  //     required: true
  //   },
  //   FirstName:{
  //     type: String,
  //     required: true
	// 	},
  //   LastName:{
  //     type: String,
  //     required: true
  //   },
  //   Email:{
  //     type: String,
  //     required: true
	// 	}
  // },
	data() {
		return{
			formData: {
        UserName: "",
        FirstName: "",
        LastName: ""
      },
		}
  },  
  created(){
    this.$eventBus.$on("EditUser", (item)=>{
      this.formData = item
    });

  },
    methods: {
      close() {
        this.$emit('close');
      },
    },
  };
</script>

<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div class="modal">
        <header class="modal-header">
          <slot name="header">
            User Infomation
            <button type="button" class="btn-close" @click="close">x</button>
          </slot>
        </header>
        <section class="modal-body">
          <slot name="body">
            <v-text-field id="UserName" label="UserName" v-model="formData.UserName"/>
            <br>
            <v-text-field id="firstName" label="First Name" v-model="formData.FirstName"/>
            <br>

            <v-text-field id="lastName" label="Last Name" v-model="formData.LastName"/>
            <br>
          </slot>
        </section>
        <footer class="modal-footer">
          <slot name="footer">
            <button type="button" class="btn-green" @click="submitData">Save changes</button>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<style>
  .modal-backdrop {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0, 0, 0, 0.3);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal {
    background: #FFFFFF;
    box-shadow: 2px 2px 20px 1px;
    overflow-x: auto;
    display: flex;
    flex-direction: column;
  }

  .modal-header,
  .modal-footer {
    padding: 15px;
    display: flex;
  }

  .modal-header {
    border-bottom: 1px solid #eeeeee;
    color: #4AAE9B;
    justify-content: space-between;
  }

  .modal-footer {
    border-top: 1px solid #eeeeee;
    justify-content: flex-end;
  }

  .modal-body {
    position: relative;
    padding: 20px 10px;
  }

  .btn-close {
    border: none;
    font-size: 20px;
    padding: 20px;
    cursor: pointer;
    font-weight: bold;
    color: #4AAE9B;
    background: transparent;
  }

  .btn-green {
    color: white;
    background: #4AAE9B;
    border: 1px solid #4AAE9B;
    border-radius: 2px;
  }
</style>