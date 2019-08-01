import { expect } from 'chai';
import module from '@/store/modules/chat';

describe('given a chat module', () => {
  describe('when getters are consumed', () => {
    it('should get no display messages if empty', () => {
      const state = {
        chatMessages: []
      };

      expect(module.getters.displayMessages(state)).to.be.empty;
    });

    it('should get display message if available', () => {
      const message = {};
      const state = {
        chatMessages: [message]
      };

      expect(module.getters.displayMessages(state)).to.contain(message);
    });
  });
  describe('when mutations are executed', () => {
    it('should add a message', () => {
      const state = {
        chatMessages: [],
        limit: 5
      };

      module.mutations.ADD_MESSAGE(state, {});

      expect(state.chatMessages.length).to.equal(1);
    });
    it('should remove the first message when limit is exceeded', () => {
      const message1 = { id: 1 };
      const message2 = { id: 2 };
      const state = {
        chatMessages: [message1],
        limit: 1
      };

      module.mutations.ADD_MESSAGE(state, message2);

      expect(state.chatMessages).to.not.contain(message1);
      expect(state.chatMessages).to.contain(message2);
    });
    it('should delete a message', () => {
      const message = { id: 1 };
      const state = {
        chatMessages: [message],
        limit: 5
      };

      module.mutations.DELETE_MESSAGE(state, message);

      expect(state.chatMessages).to.not.contain(message);
    });
  });
});
